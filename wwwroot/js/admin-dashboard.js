document.querySelectorAll('.glass-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.style.borderColor = '#333333';
    });
    card.addEventListener('mouseleave', () => {
        card.style.borderColor = '#222222';
    });
});