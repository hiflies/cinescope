const nav = document.getElementById('top-nav');
window.addEventListener('scroll', () => {
    if (window.scrollY > 50) {
        nav.classList.add('bg-surface/80', 'backdrop-blur-md', 'border-surface-container-highest', 'py-3');
        nav.classList.remove('bg-surface/0', 'border-transparent', 'py-4');
    } else {
        nav.classList.remove('bg-surface/80', 'backdrop-blur-md', 'border-surface-container-highest', 'py-3');
        nav.classList.add('bg-surface/0', 'border-transparent', 'py-4');
    }
});