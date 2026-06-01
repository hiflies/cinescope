// Simple Interaction Logic
document.getElementById('selectAll').addEventListener('change', function(e) {
    const checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
    checkboxes.forEach(cb => cb.checked = e.target.checked);
});

// Add visual scale to buttons
const buttons = document.querySelectorAll('button');
buttons.forEach(btn => {
    btn.addEventListener('mousedown', () => btn.classList.add('scale-95'));
    btn.addEventListener('mouseup', () => btn.classList.remove('scale-95'));
    btn.addEventListener('mouseleave', () => btn.classList.remove('scale-95'));
});