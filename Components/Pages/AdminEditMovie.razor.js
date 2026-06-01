// Simple micro-interaction for Save button
document.querySelectorAll('button').forEach(btn => {
    btn.addEventListener('click', function(e) {
        if (this.innerText.includes('Save Changes')) {
            const originalContent = this.innerHTML;
            this.innerHTML = '<span class="material-symbols-outlined animate-spin">sync</span> Saving...';
            this.disabled = true;
            setTimeout(() => {
                this.innerHTML = '<span class="material-symbols-outlined">check_circle</span> Changes Saved!';
                this.classList.remove('bg-primary-container');
                this.classList.add('bg-green-600');
                setTimeout(() => {
                    this.innerHTML = originalContent;
                    this.classList.add('bg-primary-container');
                    this.classList.remove('bg-green-600');
                    this.disabled = false;
                }, 2000);
            }, 1500);
        }
    });
});

// Hover effect for poster upload zone
const mediaZones = document.querySelectorAll('.aspect-video, .aspect-\\[2\\/3\\]');
mediaZones.forEach(zone => {
    zone.addEventListener('mouseenter', () => {
        zone.classList.add('scale-[1.01]');
        zone.style.transition = 'all 0.3s ease';
    });
    zone.addEventListener('mouseleave', () => {
        zone.classList.remove('scale-[1.01]');
    });
});