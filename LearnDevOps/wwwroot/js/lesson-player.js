/**
 * DevOps Quest — lesson-player.js
 * AJAX game loop for the lesson play page.
 * Expects LESSON_ID and TOTAL_QUESTIONS to be defined before this script loads.
 */
(function () {
    'use strict';

    /* ── DOM refs ─────────────────────────────────────────── */
    const questionArea  = document.getElementById('questionArea');
    const progressFill  = document.getElementById('progressFill');
    const livesDisplay  = document.getElementById('livesDisplay');
    const actionBar     = document.getElementById('actionBar');
    const checkBtn      = document.getElementById('checkBtn');
    const feedbackBanner= document.getElementById('feedbackBanner');
    const feedbackIcon  = document.getElementById('feedbackIcon');
    const feedbackLabel = document.getElementById('feedbackLabel');
    const feedbackExp   = document.getElementById('feedbackExplanation');
    const continueBtn   = document.getElementById('continueBtn');

    /* ── State ────────────────────────────────────────────── */
    let currentQuestion  = null;
    let selectedAnswerId = null;
    let selectedText     = null;
    let pairMatches      = [];      // [{leftId, rightId}]
    let selectedLeftId   = null;    // for match-pairs
    let waitingForContinue = false;

    /* ── Bootstrap ────────────────────────────────────────── */
    loadNextQuestion();

    continueBtn.addEventListener('click', () => {
        if (!waitingForContinue) return;
        hideFeedback();
        loadNextQuestion();
    });

    checkBtn.addEventListener('click', () => {
        if (waitingForContinue) return;
        submitAnswer();
    });

    /* ── Load question ────────────────────────────────────── */
    async function loadNextQuestion() {
        showLoading();
        try {
            const res = await fetch(`/lesson/${LESSON_ID}/question`);
            if (!res.ok) throw new Error('Network error');
            const data = await res.json();

            if (data.complete) {
                window.location.href = `/lesson/${LESSON_ID}/complete`;
                return;
            }

            currentQuestion  = data;
            selectedAnswerId = null;
            selectedText     = null;
            pairMatches      = [];
            selectedLeftId   = null;

            renderQuestion(data);
            updateProgress(data.questionNumber - 1, data.totalQuestions);
            updateLives(data.livesRemaining);
        } catch (e) {
            questionArea.innerHTML = `<div class="alert alert-danger">Error loading question. <a href="/lesson/${LESSON_ID}/start">Restart?</a></div>`;
        }
    }

    /* ── Render question ──────────────────────────────────── */
    function renderQuestion(q) {
        const letters = 'ABCD';
        let optionsHtml = '';

        switch (q.type) {
            case 0: // MultipleChoice
            case 1: // TrueFalse
                optionsHtml = `<div class="dq-options-grid ${q.type === 1 ? 'dq-tf-grid' : ''}">`;
                q.options.forEach((opt, i) => {
                    const letter = letters[i] || (i + 1);
                    optionsHtml += `
                        <button class="dq-option-btn" data-id="${opt.id}" onclick="window._dqSelectOption(this)">
                            <span class="dq-option-letter">${letter}</span>
                            <span>${escapeHtml(opt.text)}</span>
                        </button>`;
                });
                optionsHtml += '</div>';
                break;

            case 2: // FillInTheBlank
                optionsHtml = `
                    <div>
                        <input class="dq-fitb-input" id="fitbInput" type="text"
                               placeholder="Type your answer..." autocomplete="off"
                               oninput="window._dqFitbInput(this)" />
                    </div>`;
                break;

            case 3: // MatchPairs
                const left  = q.leftPairs  || [];
                const right = q.rightPairs || [];
                let leftCols = left.map(o =>
                    `<div class="dq-pair-item" data-id="${o.id}" data-side="left" onclick="window._dqPairClick(this)">${escapeHtml(o.text)}</div>`
                ).join('');
                let rightCols = right.map(o =>
                    `<div class="dq-pair-item" data-id="${o.id}" data-side="right" onclick="window._dqPairClick(this)">${escapeHtml(o.text)}</div>`
                ).join('');
                optionsHtml = `
                    <div class="dq-pairs-grid">
                        <div class="dq-pair-col">${leftCols}</div>
                        <div class="dq-pair-col">${rightCols}</div>
                    </div>
                    <div class="mt-2 small text-muted">Click a left item, then its match on the right.</div>`;
                break;

            default:
                optionsHtml = '<p class="text-muted">Unknown question type.</p>';
        }

        const hintHtml = q.hintText
            ? `<div class="dq-hint mt-2 mb-3">💡 Hint: ${escapeHtml(q.hintText)}</div>`
            : '';

        const codeHtml = q.codeSnippet
            ? `<pre class="dq-code-block">${escapeHtml(q.codeSnippet)}</pre>`
            : '';

        const typeLabel = ['Multiple Choice', 'True or False', 'Fill in the Blank', 'Match the Pairs'][q.type] || 'Question';

        questionArea.innerHTML = `
            <div class="dq-question-card">
                <div class="dq-q-meta">${typeLabel} · Q${q.questionNumber} of ${q.totalQuestions}</div>
                <div class="dq-q-prompt">${escapeHtml(q.prompt)}</div>
                ${codeHtml}
                ${optionsHtml}
                ${hintHtml}
            </div>`;

        // For fill-in-the-blank: auto-focus and handle Enter
        if (q.type === 2) {
            const inp = document.getElementById('fitbInput');
            inp.focus();
            inp.addEventListener('keydown', e => { if (e.key === 'Enter') submitAnswer(); });
        }

        actionBar.style.display = 'block';
        checkBtn.disabled = (q.type !== 3 || true); // starts disabled
        // For MatchPairs enable check only when all pairs matched
        if (q.type === 3) updatePairCheckBtn();
    }

    /* ── Option selection handlers (MC / TF) ─────────────── */
    window._dqSelectOption = function (btn) {
        if (waitingForContinue) return;
        document.querySelectorAll('.dq-option-btn').forEach(b => b.classList.remove('selected'));
        btn.classList.add('selected');
        selectedAnswerId = parseInt(btn.dataset.id);
        checkBtn.disabled = false;
    };

    /* ── Fill-in-blank handler ────────────────────────────── */
    window._dqFitbInput = function (inp) {
        if (waitingForContinue) return;
        selectedText = inp.value.trim();
        checkBtn.disabled = selectedText.length === 0;
    };

    /* ── Pair click handler ───────────────────────────────── */
    window._dqPairClick = function (item) {
        if (waitingForContinue) return;
        const side = item.dataset.side;
        const id   = parseInt(item.dataset.id);

        if (item.classList.contains('matched')) return;

        if (side === 'left') {
            // Deselect previous left
            document.querySelectorAll('.dq-pair-item[data-side="left"]').forEach(el => el.classList.remove('selected-left'));
            item.classList.add('selected-left');
            selectedLeftId = id;
        } else {
            if (selectedLeftId == null) return;
            // Form a pair
            const leftItem = document.querySelector(`.dq-pair-item[data-side="left"][data-id="${selectedLeftId}"]`);
            pairMatches.push({ leftId: selectedLeftId, rightId: id });
            if (leftItem)  leftItem.classList.replace('selected-left', 'matched');
            item.classList.add('matched');
            selectedLeftId = null;
            updatePairCheckBtn();
        }
    };

    function updatePairCheckBtn() {
        const leftTotal = (currentQuestion?.leftPairs?.length) || 0;
        checkBtn.disabled = pairMatches.length < leftTotal;
    }

    /* ── Submit answer ────────────────────────────────────── */
    async function submitAnswer() {
        if (waitingForContinue) return;

        const body = {
            questionId:  currentQuestion.questionId,
            answerId:    selectedAnswerId,
            answerText:  selectedText,
            pairMatches: pairMatches.length > 0 ? pairMatches : null
        };

        // Disable UI while submitting
        checkBtn.disabled = true;
        document.querySelectorAll('.dq-option-btn, .dq-fitb-input, .dq-pair-item').forEach(el => el.disabled = true);

        try {
            const res = await fetch(`/lesson/${LESSON_ID}/answer`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body)
            });

            if (!res.ok) throw new Error('Submit failed');
            const result = await res.json();
            handleResult(result);
        } catch (e) {
            checkBtn.disabled = false;
            alert('Error submitting answer. Please try again.');
        }
    }

    /* ── Handle result ────────────────────────────────────── */
    function handleResult(result) {
        waitingForContinue = true;
        actionBar.style.display = 'none';

        updateLives(result.livesRemaining);

        // Highlight options
        if (currentQuestion.type === 0 || currentQuestion.type === 1) {
            document.querySelectorAll('.dq-option-btn').forEach(btn => {
                const bid = parseInt(btn.dataset.id);
                if (bid === result.correctAnswerId) {
                    btn.classList.add('correct');
                } else if (bid === selectedAnswerId && !result.isCorrect) {
                    btn.classList.add('incorrect');
                }
            });
        }

        if (currentQuestion.type === 2) {
            const inp = document.getElementById('fitbInput');
            if (inp) inp.classList.add(result.isCorrect ? 'correct' : 'incorrect');
        }

        showFeedback(result);

        if (result.isLessonComplete || result.outOfLives) {
            continueBtn.textContent = 'See Results';
            continueBtn.onclick = () => {
                window.location.href = `/lesson/${LESSON_ID}/complete`;
            };
        }
    }

    /* ── Feedback banner ─────────────────────────────────── */
    function showFeedback(result) {
        if (result.isCorrect) {
            feedbackBanner.className = 'dq-feedback-banner correct';
            feedbackIcon.textContent = '✅';
            feedbackLabel.textContent = 'Correct!';
            feedbackLabel.style.color = 'var(--dq-green)';
            showXpPop('+XP');
        } else {
            feedbackBanner.className = 'dq-feedback-banner incorrect';
            feedbackIcon.textContent = '❌';
            feedbackLabel.textContent = result.outOfLives ? 'Out of lives!' : 'Oops!';
            feedbackLabel.style.color = 'var(--dq-red)';
        }
        feedbackExp.textContent = result.explanation || '';
        feedbackBanner.style.display = 'block';
    }

    function hideFeedback() {
        feedbackBanner.style.display = 'none';
        waitingForContinue = false;
    }

    /* ── Progress & Lives UI ─────────────────────────────── */
    function updateProgress(done, total) {
        const pct = total > 0 ? (done / total * 100) : 0;
        progressFill.style.width = pct + '%';
    }

    function updateLives(remaining) {
        let html = '';
        for (let i = 0; i < 3; i++) {
            html += i < remaining
                ? '<span class="dq-heart">❤️</span>'
                : '<span class="dq-heart dq-heart--lost">🖤</span>';
        }
        livesDisplay.innerHTML = html;
    }

    /* ── XP pop float ────────────────────────────────────── */
    function showXpPop(text) {
        const el = document.createElement('div');
        el.className = 'dq-xp-pop';
        el.textContent = text;
        el.style.left = Math.random() * 60 + 20 + '%';
        el.style.bottom = '80px';
        document.body.appendChild(el);
        setTimeout(() => el.remove(), 1300);
    }

    /* ── Helpers ─────────────────────────────────────────── */
    function showLoading() {
        questionArea.innerHTML = `
            <div class="dq-loading text-center py-5">
                <div class="spinner-border text-success" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <div class="mt-2 text-muted">Loading question...</div>
            </div>`;
        actionBar.style.display = 'none';
        feedbackBanner.style.display = 'none';
    }

    function escapeHtml(str) {
        if (!str) return '';
        return str
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#039;');
    }
})();
