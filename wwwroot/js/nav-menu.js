const themeToggle = document.getElementById('theme-toggle');
const themeIcon = document.getElementById('theme-icon');

function updateThemeIcon() {
    const isLight = document.documentElement.classList.contains('light');
    themeIcon.textContent = isLight ? 'dark_mode' : 'light_mode';
}

updateThemeIcon();

themeToggle.addEventListener('click', () => {
    const isLight = document.documentElement.classList.contains('light');
    if (!isLight) {
        document.documentElement.classList.add('light');
        localStorage.setItem('theme', 'light');
    } else {
        document.documentElement.classList.remove('light');
        localStorage.setItem('theme', 'dark');
    }
    updateThemeIcon();
});

const nav = document.getElementById('top-nav');
const navLoginBar = document.getElementById('nav-login-bar');
window.addEventListener('scroll', () => {
    if (window.scrollY > 50) {
        nav.classList.add('bg-surface/80', 'backdrop-blur-md', 'border-surface-container-highest', 'py-3');
        nav.classList.remove('bg-surface/0', 'border-transparent', 'py-4');

        if (navLoginBar != null) {
            navLoginBar.classList.remove('bg-surface/80', 'backdrop-blur-md');
        }
    } else {
        nav.classList.remove('bg-surface/80', 'backdrop-blur-md', 'border-surface-container-highest', 'py-3');
        nav.classList.add('bg-surface/0', 'border-transparent', 'py-4');

        if (navLoginBar != null) {
            navLoginBar.classList.add('bg-surface/80', 'backdrop-blur-md');
        }
    }
});