// Simple interactive demo for filter chips
document.querySelectorAll('.flex.items-center.gap-xs.px-md.py-1').forEach(chip => {
    chip.querySelector('.material-symbols-outlined').addEventListener('click', (e) => {
        e.stopPropagation();
        chip.style.opacity = '0';
        setTimeout(() => chip.remove(), 300);
    });
});

// Search bar interaction
const searchInput = document.querySelector('input[type="text"]');
searchInput.addEventListener('focus', () => {
    searchInput.parentElement.classList.add('ring-2', 'ring-primary/20');
});
searchInput.addEventListener('blur', () => {
    searchInput.parentElement.classList.remove('ring-2', 'ring-primary/20');
});