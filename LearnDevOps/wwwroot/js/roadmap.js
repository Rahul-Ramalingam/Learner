(function () {
    'use strict';

    var slug = document.querySelector('[data-roadmap-slug]')?.dataset.roadmapSlug || '';

    document.addEventListener('click', function (e) {
        // Topic click
        var topicEl = e.target.closest('.rm-topic');
        if (topicEl) {
            var topicId = topicEl.dataset.topicId;
            if (topicId && slug) {
                window.location.href = '/roadmap/' + encodeURIComponent(slug) + '/topic/' + encodeURIComponent(topicId);
            }
            return;
        }

        // Project card click (section or capstone)
        var projectEl = e.target.closest('.rm-project-card, .rm-capstone-card');
        if (projectEl) {
            var projectId = projectEl.dataset.projectId;
            if (projectId && slug) {
                window.location.href = '/roadmap/' + encodeURIComponent(slug) + '/project/' + encodeURIComponent(projectId);
            }
            return;
        }
    });
})();
