document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');
    const sidebarCollapse = document.getElementById('sidebarCollapse');
    const sidebarToggleIcon = document.getElementById('sidebarToggleIcon');
    const mainContent = document.getElementById('main-content');

    if (!sidebar) {
        return;
    }

    const mobileToggle = document.createElement('button');
    mobileToggle.id = 'mobileSidebarToggle';
    mobileToggle.className = 'btn btn-sm d-md-none position-fixed';
    mobileToggle.style.cssText = 'top: 1rem; right: 1rem; z-index: 1001; background: var(--purple-primary); color: white; border: none; border-radius: 8px; padding: 0.5rem 0.75rem; box-shadow: 0 2px 8px rgba(0,0,0,0.15);';
    mobileToggle.innerHTML = '<i class="bi bi-list fs-5"></i>';
    mobileToggle.setAttribute('aria-label', 'Toggle sidebar');
    document.body.appendChild(mobileToggle);

    const overlay = document.createElement('div');
    overlay.id = 'sidebarOverlay';
    overlay.className = 'sidebar-overlay';
    document.body.appendChild(overlay);

    function isMobile() {
        return window.innerWidth <= 768;
    }

    function toggleSidebar() {
        if (isMobile()) {
            sidebar.classList.toggle('show');
            overlay.classList.toggle('show');

            const icon = mobileToggle.querySelector('i');
            if (sidebar.classList.contains('show')) {
                icon.classList.remove('bi-list');
                icon.classList.add('bi-x');
            } else {
                icon.classList.remove('bi-x');
                icon.classList.add('bi-list');
            }
        } else {
            sidebar.classList.toggle('sidebar-collapsed');
            
            if (sidebar.classList.contains('sidebar-collapsed')) {
                sidebarToggleIcon.classList.add('bi-list');
                sidebarToggleIcon.classList.remove('bi-x');
            } else {
                sidebarToggleIcon.classList.add('bi-x');
                sidebarToggleIcon.classList.remove('bi-list');
            }
        }
    }

    function closeSidebar() {
        if (isMobile() && sidebar.classList.contains('show')) {
            sidebar.classList.remove('show');
            overlay.classList.remove('show');
            const icon = mobileToggle.querySelector('i');
            icon.classList.remove('bi-x');
            icon.classList.add('bi-list');
        }
    }

    if (sidebarCollapse && sidebar) {
        sidebarCollapse.addEventListener('click', function (e) {
            e.stopPropagation();
            toggleSidebar();
        });
    }

    mobileToggle.addEventListener('click', function (e) {
        e.stopPropagation();
        toggleSidebar();
    });

    overlay.addEventListener('click', function () {
        closeSidebar();
    });

    const sidebarLinks = sidebar.querySelectorAll('.sidebar-link');
    sidebarLinks.forEach(link => {
        link.addEventListener('click', function () {
            if (isMobile()) {
                closeSidebar();
            }
        });
    });

    function updateMobileToggleVisibility() {
        if (isMobile()) {
            mobileToggle.style.display = 'block';
            if (sidebarCollapse) {
                sidebarCollapse.style.display = 'none';
            }
        } else {
            mobileToggle.style.display = 'none';
            sidebar.classList.remove('show');
            overlay.classList.remove('show');
            if (sidebarCollapse) {
                sidebarCollapse.style.display = 'flex';
            }
            sidebar.classList.remove('sidebar-collapsed');
        }
    }

    let resizeTimer;
    function handleResize() {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(function () {
            updateMobileToggleVisibility();
            if (!isMobile()) {
                const icon = mobileToggle.querySelector('i');
                icon.classList.remove('bi-x');
                icon.classList.add('bi-list');
            }
        }, 250);
    }

    window.addEventListener('resize', handleResize);
    
    updateMobileToggleVisibility();
});
