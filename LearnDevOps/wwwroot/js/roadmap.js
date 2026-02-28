(function () {
    'use strict';

    var slug = document.querySelector('[data-roadmap-slug]')?.dataset.roadmapSlug || '';

    document.addEventListener('click', function (e) {
        var topicEl = e.target.closest('.rm-topic');
        if (!topicEl) return;
        var topicId = topicEl.dataset.topicId;
        if (!topicId || !slug) return;
        window.location.href = '/roadmap/' + encodeURIComponent(slug) + '/topic/' + encodeURIComponent(topicId);
    });
})();
