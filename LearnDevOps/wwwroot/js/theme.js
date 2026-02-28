/**
 * DevOps Quest — theme.js
 * Dark / light mode toggle with localStorage persistence.
 */
(function () {
    'use strict';

    const STORAGE_KEY = 'dq-theme';
    const DARK  = 'dark';
    const LIGHT = 'light';

    /* Apply theme to <html> immediately (called inline too, but safe to call again) */
    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
    }

    /* Sync all toggle button icons on the page */
    function syncButtons(theme) {
        document.querySelectorAll('.dq-theme-toggle').forEach(btn => {
            btn.innerHTML = theme === DARK
                ? '<i class="bi bi-sun-fill"></i>'
                : '<i class="bi bi-moon-fill"></i>';
            btn.title = theme === DARK ? 'Switch to light mode' : 'Switch to dark mode';
            btn.setAttribute('aria-label', btn.title);
        });
    }

    /* Toggle and persist */
    function toggle() {
        const current = document.documentElement.getAttribute('data-theme') || LIGHT;
        const next    = current === DARK ? LIGHT : DARK;
        applyTheme(next);
        syncButtons(next);
        try { localStorage.setItem(STORAGE_KEY, next); } catch (_) {}
    }

    /* Wire up any buttons already in DOM, and any added later */
    function wireButtons() {
        document.querySelectorAll('.dq-theme-toggle').forEach(btn => {
            if (!btn.dataset.themeWired) {
                btn.addEventListener('click', toggle);
                btn.dataset.themeWired = '1';
            }
        });
        syncButtons(document.documentElement.getAttribute('data-theme') || LIGHT);
    }

    /* Run on DOMContentLoaded */
    document.addEventListener('DOMContentLoaded', wireButtons);

    /* Expose globally so layouts can call wireButtons after dynamic injection */
    window.DQTheme = { toggle, wireButtons };
})();
