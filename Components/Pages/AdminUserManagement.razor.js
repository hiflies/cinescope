// Micro-interactions for table checkboxes
const masterCheckbox = document.querySelector('thead input[type="checkbox"]');
const userCheckboxes = document.querySelectorAll('tbody input[type="checkbox"]');
const selectedCountSpan = document.querySelector('.text-primary.font-bold');

masterCheckbox?.addEventListener('change', (e) => {
    userCheckboxes.forEach(cb => {
        cb.checked = e.target.checked;
        cb.closest('tr').classList.toggle('bg-primary-container/5', e.target.checked);
    });
    updateCount();
});

userCheckboxes.forEach(cb => {
    cb.addEventListener('change', (e) => {
        cb.closest('tr').classList.toggle('bg-primary-container/5', e.target.checked);
        updateCount();
    });
});

function updateCount() {
    const count = Array.from(userCheckboxes).filter(cb => cb.checked).length;
    if(selectedCountSpan) selectedCountSpan.textContent = count;
}

// Simulating search focus
const searchInput = document.querySelector('input[placeholder="Search system..."]');
searchInput?.addEventListener('focus', () => {
    searchInput.classList.add('w-80');
});
searchInput?.addEventListener('blur', () => {
    searchInput.classList.remove('w-80');
});