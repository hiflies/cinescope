// Simple intersection observer for a reveal effect on cards

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('opacity-100', 'translate-y-0');
            entry.target.classList.remove('opacity-0', 'translate-y-4');
        }
    });
}, {
    threshold: 0.1
});

document.querySelectorAll('.movie-card').forEach(card => {
    card.classList.add('opacity-0', 'translate-y-4', 'transition-all', 'duration-700');
    observer.observe(card);
});