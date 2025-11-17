// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Sidebar toggle functionality
document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');
    const sidebarCollapse = document.getElementById('sidebarCollapse');
    const sidebarToggleIcon = document.getElementById('sidebarToggleIcon');
    const mainContent = document.getElementById('main-content');

    if (sidebarCollapse && sidebar) {
        sidebarCollapse.addEventListener('click', function () {
            sidebar.classList.toggle('sidebar-collapsed');
            
            // Update icon
            if (sidebar.classList.contains('sidebar-collapsed')) {
                sidebarToggleIcon.classList.add('bi-list');
                sidebarToggleIcon.classList.remove('bi-x');
            } else {
                sidebarToggleIcon.classList.add('bi-x');
                sidebarToggleIcon.classList.remove('bi-list');
            }
        });
    }

    // Mobile sidebar toggle
    if (window.innerWidth <= 768) {
        const mobileToggle = document.createElement('button');
        mobileToggle.className = 'btn btn-sm d-md-none position-fixed';
        mobileToggle.style.cssText = 'top: 1rem; left: 1rem; z-index: 101; background: var(--purple-primary); color: white; border: none;';
        mobileToggle.innerHTML = '<i class="bi bi-list"></i>';
        mobileToggle.addEventListener('click', function () {
            sidebar.classList.toggle('show');
        });
        document.body.appendChild(mobileToggle);
    }
});
