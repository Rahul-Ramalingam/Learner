/**
 * DevOps Quest — animations.js
 * Progress-bar entrance, number count-up, and confetti burst.
 */
(function () {
    'use strict';

    /* ── Count-up ─────────────────────────────────────────────
       Animates el's text from 0 → target over `duration` ms,
       starting after `delay` ms.  Preserves a leading/trailing
       non-numeric suffix (e.g. " XP", "%").
    ──────────────────────────────────────────────────────────── */
    function countUp(el, target, duration, delay, suffix) {
        suffix = suffix || '';
        setTimeout(function () {
            var start = performance.now();
            function step(now) {
                var p    = Math.min((now - start) / duration, 1);
                var ease = 1 - Math.pow(1 - p, 3);          // ease-out cubic
                el.textContent = Math.round(target * ease).toLocaleString() + suffix;
                if (p < 1) requestAnimationFrame(step);
            }
            requestAnimationFrame(step);
        }, delay || 0);
    }

    /* ── Progress bars: animate from 0 on page load ───────── */
    function animateProgressBars() {
        var bars = document.querySelectorAll(
            '.dq-progress-fill, .dq-xp-bar-fill, .dq-unit-progress-fill'
        );
        bars.forEach(function (bar) {
            var target = bar.style.width;
            if (!target || target === '0%') return;
            bar.style.transition = 'none';
            bar.style.width      = '0%';
            // Double rAF ensures the browser paints the 0% frame first
            requestAnimationFrame(function () {
                requestAnimationFrame(function () {
                    bar.style.transition = '';
                    bar.style.width      = target;
                });
            });
        });
    }

    /* ── Number count-up: scan known stat elements ─────────── */
    function animateStats() {
        var selectors = [
            '.dq-xp-reward-amount',          // complete screen big XP number
            '.dq-complete-card .dq-stat-val', // complete screen mini stats
            '.dq-global-stats .dq-stat-value', // home page global stats
            '.dq-profile-header .dq-stat-value' // profile header stats
        ];
        selectors.forEach(function (sel) {
            document.querySelectorAll(sel).forEach(function (el) {
                var raw    = el.textContent.trim();
                var num    = parseInt(raw.replace(/[^0-9]/g, ''), 10);
                var suffix = raw.replace(/[0-9,]/g, '');
                if (!isNaN(num) && num > 0) {
                    el.textContent = '0' + suffix;
                    countUp(el, num, 900, 320, suffix);
                }
            });
        });
    }

    /* ── Confetti burst for lesson success screen ─────────── */
    function injectConfettiKeyframes() {
        if (document.getElementById('dq-confetti-kf')) return;
        var s = document.createElement('style');
        s.id  = 'dq-confetti-kf';
        s.textContent =
            '@keyframes dqConftFall {' +
            '  0%  { transform: translateY(0)     rotate(0deg);   opacity: 1; }' +
            '  85% {                                               opacity: 1; }' +
            '  100%{ transform: translateY(105vh) rotate(720deg); opacity: 0; }' +
            '}';
        document.head.appendChild(s);
    }

    function confettiBurst() {
        var colors  = ['#58CC02','#FFD900','#1CB0F6','#FF4B4B','#FF8C00','#a855f7'];
        var count   = 60;
        var frag    = document.createDocumentFragment();

        for (var i = 0; i < count; i++) {
            var p      = document.createElement('div');
            var size   = 4 + Math.random() * 7;
            var circle = Math.random() > .45;
            p.style.cssText = [
                'position:fixed',
                'width:'  + size + 'px',
                'height:' + (circle ? size : size * 2.2) + 'px',
                'background:' + colors[Math.floor(Math.random() * colors.length)],
                'border-radius:' + (circle ? '50%' : '3px'),
                'left:' + (4 + Math.random() * 92) + '%',
                'top:-14px',
                'pointer-events:none',
                'z-index:9998',
                'animation:dqConftFall ' + (1.3 + Math.random() * 1.4) + 's ease-in ' +
                    (Math.random() * .65) + 's forwards'
            ].join(';');
            frag.appendChild(p);
        }
        document.body.appendChild(frag);

        // Clean up after animations finish
        setTimeout(function () {
            document.querySelectorAll('[style*="dqConftFall"]').forEach(function (el) {
                el.remove();
            });
        }, 3200);
    }

    /* ── Wire everything up on DOMContentLoaded ─────────────── */
    document.addEventListener('DOMContentLoaded', function () {
        animateProgressBars();
        animateStats();
        injectConfettiKeyframes();

        // Fire confetti only on the success complete screen
        if (document.querySelector('.dq-complete-card--success')) {
            setTimeout(confettiBurst, 450);
        }
    });

    // Expose for external use (e.g. lesson-player.js can call after XP update)
    window.DQAnimations = {
        countUp          : countUp,
        animateProgressBars: animateProgressBars,
        confettiBurst    : confettiBurst
    };

})();
