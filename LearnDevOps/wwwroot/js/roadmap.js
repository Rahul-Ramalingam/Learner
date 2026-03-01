(function () {
    'use strict';

    var slug = document.querySelector('[data-roadmap-slug]')?.dataset.roadmapSlug || '';

    /* ── Navigation ── */
    document.addEventListener('click', function (e) {
        var topicEl = e.target.closest('.rm-topic');
        if (topicEl) {
            var topicId = topicEl.dataset.topicId;
            if (topicId && slug) {
                // Quick ripple before navigating
                spawnRipple(topicEl, e);
                setTimeout(function () {
                    window.location.href = '/roadmap/' + encodeURIComponent(slug) + '/topic/' + encodeURIComponent(topicId);
                }, 160);
            }
            return;
        }

        var projectEl = e.target.closest('.rm-project-card, .rm-capstone-card');
        if (projectEl) {
            var projectId = projectEl.dataset.projectId;
            if (projectId && slug) {
                window.location.href = '/roadmap/' + encodeURIComponent(slug) + '/project/' + encodeURIComponent(projectId);
            }
            return;
        }
    });

    /* ── Mark the very first uncompleted, non-skipped topic as "Next Quest" ── */
    function highlightNextQuest() {
        var topics = document.querySelectorAll('.rm-topic[data-topic-id]');
        var found = false;
        for (var i = 0; i < topics.length; i++) {
            var t = topics[i];
            var isDone       = t.classList.contains('rm-topic--done');
            var isInProgress = t.classList.contains('rm-topic--in-progress');
            var isSkip       = t.classList.contains('rm-topic--skip');
            var isOptional   = t.classList.contains('rm-topic--optional');
            if (!isDone && !isInProgress && !isSkip && !isOptional && !found) {
                t.classList.add('rm-topic--next');
                found = true;
            }
        }
    }

    /* ── Animate stat numbers counting up ── */
    function animateCounter(el, target, duration) {
        if (!el) return;
        var start = 0;
        var startTime = null;
        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            // Ease out cubic
            var eased = 1 - Math.pow(1 - progress, 3);
            el.textContent = Math.round(eased * target);
            if (progress < 1) requestAnimationFrame(step);
        }
        requestAnimationFrame(step);
    }

    /* ── Animate XP bar width ── */
    function animateXpBar() {
        var fill = document.getElementById('rmProgressFill');
        if (!fill) return;
        var targetWidth = fill.style.width;
        fill.style.width = '0%';
        // Let CSS transition handle it after a brief moment
        requestAnimationFrame(function () {
            requestAnimationFrame(function () {
                fill.style.width = targetWidth;
            });
        });
    }

    /* ── Ripple effect on topic click ── */
    function spawnRipple(el, e) {
        var rect = el.getBoundingClientRect();
        var ripple = document.createElement('span');
        ripple.style.cssText = [
            'position:absolute',
            'border-radius:50%',
            'background:rgba(255,255,255,.45)',
            'width:60px',
            'height:60px',
            'left:' + (e.clientX - rect.left - 30) + 'px',
            'top:' + (e.clientY - rect.top - 30) + 'px',
            'transform:scale(0)',
            'transition:transform .35s ease,opacity .35s ease',
            'opacity:1',
            'pointer-events:none',
            'z-index:10'
        ].join(';');
        el.appendChild(ripple);
        requestAnimationFrame(function () {
            ripple.style.transform = 'scale(2.5)';
            ripple.style.opacity = '0';
        });
        setTimeout(function () { ripple.remove(); }, 400);
    }

    /* ── Show toast when returning after a completion ── */
    function checkReturnToast() {
        var justDone = sessionStorage.getItem('rm_just_done_' + slug);
        if (!justDone) return;
        sessionStorage.removeItem('rm_just_done_' + slug);

        var toast = document.createElement('div');
        toast.className = 'rm-achievement-toast';
        toast.innerHTML = '<span class="rm-achievement-toast-icon">⭐</span><span>Quest cleared! <strong>' + justDone + '</strong> marked as done.</span>';
        document.body.appendChild(toast);
        requestAnimationFrame(function () {
            requestAnimationFrame(function () {
                toast.classList.add('rm-achievement-toast--show');
            });
        });
        setTimeout(function () {
            toast.classList.remove('rm-achievement-toast--show');
            setTimeout(function () { toast.remove(); }, 500);
        }, 3500);
    }

    /* ── Init ── */
    document.addEventListener('DOMContentLoaded', function () {
        highlightNextQuest();
        animateXpBar();
        checkReturnToast();

        // Animate done counter
        var doneEl = document.getElementById('rmDoneCount');
        if (doneEl) {
            var target = parseInt(doneEl.textContent, 10) || 0;
            if (target > 0) animateCounter(doneEl, target, 900);
        }
    });

})();
