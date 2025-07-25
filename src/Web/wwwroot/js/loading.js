/**
 * Global Loading Sistemi
 * Tüm AJAX isteklerine otomatik loading ekler
 */

class LoadingManager {
    constructor() {
        this.overlay = null;
        this.init();
    }

    init() {
        // Loading overlay oluştur
        this.createOverlay();
        
        // AJAX interceptor'ları ekle
        this.setupAjaxInterceptors();
        
        // Form submit interceptor'ları ekle
        this.setupFormInterceptors();
    }

    createOverlay() {
        // Eğer zaten varsa oluşturma
        if (document.getElementById('globalLoadingOverlay')) {
            this.overlay = document.getElementById('globalLoadingOverlay');
            return;
        }

        this.overlay = document.createElement('div');
        this.overlay.id = 'globalLoadingOverlay';
        this.overlay.className = 'position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-none';
        this.overlay.style.cssText = 'z-index: 9999; backdrop-filter: blur(2px);';
        
        this.overlay.innerHTML = `
            <div class="d-flex justify-content-center align-items-center h-100">
                <div class="text-center text-white">
                    <div class="spinner-border mb-3" style="width: 3rem; height: 3rem;" role="status">
                        <span class="visually-hidden">Yükleniyor...</span>
                    </div>
                    <p class="mb-0 fs-5">İşleminiz gerçekleştiriliyor...</p>
                </div>
            </div>
        `;
        
        document.body.appendChild(this.overlay);
    }

    show() {
        if (this.overlay) {
            this.overlay.classList.remove('d-none');
            document.body.style.overflow = 'hidden';
        }
    }

    hide() {
        if (this.overlay) {
            this.overlay.classList.add('d-none');
            document.body.style.overflow = '';
        }
    }

    setupAjaxInterceptors() {
        // Fetch API interceptor
        const originalFetch = window.fetch;
        let activeRequests = 0;

        window.fetch = async (...args) => {
            activeRequests++;
            this.show();

            try {
                const response = await originalFetch(...args);
                return response;
            } finally {
                activeRequests--;
                if (activeRequests === 0) {
                    this.hide();
                }
            }
        };

        // XMLHttpRequest interceptor (eski kodlar için)
        const originalXHROpen = XMLHttpRequest.prototype.open;
        const originalXHRSend = XMLHttpRequest.prototype.send;
        let xhrCount = 0;

        XMLHttpRequest.prototype.open = function(...args) {
            this._loadingActive = true;
            xhrCount++;
            loadingManager.show();
            return originalXHROpen.apply(this, args);
        };

        XMLHttpRequest.prototype.send = function(...args) {
            const xhr = this;
            const originalOnReadyStateChange = xhr.onreadystatechange;

            xhr.onreadystatechange = function() {
                if (xhr.readyState === 4) {
                    xhrCount--;
                    if (xhrCount === 0) {
                        loadingManager.hide();
                    }
                }
                if (originalOnReadyStateChange) {
                    originalOnReadyStateChange.apply(xhr, arguments);
                }
            };

            return originalXHRSend.apply(this, args);
        };
    }

    setupFormInterceptors() {
        // Tüm form submit'lerini yakala
        document.addEventListener('submit', (e) => {
            const form = e.target;
            
            // Eğer form zaten loading göstermiyorsa
            if (!form.classList.contains('loading-active')) {
                form.classList.add('loading-active');
                
                // Form submit butonunu devre dışı bırak
                const submitBtn = form.querySelector('button[type="submit"]');
                if (submitBtn) {
                    submitBtn.disabled = true;
                    submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>İşleniyor...';
                }
            }
        });
    }
}

// Global alert sistemi
class AlertManager {
    static show(type, message, duration = 5000) {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
        alertDiv.style.cssText = 'top: 20px; right: 20px; z-index: 10000; min-width: 300px; max-width: 400px; box-shadow: 0 4px 12px rgba(0,0,0,0.15);';
        
        alertDiv.innerHTML = `
            <div class="d-flex align-items-center">
                <div class="flex-grow-1">
                    ${this.getIcon(type)} ${message}
                </div>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        
        document.body.appendChild(alertDiv);
        
        // Otomatik kaldır
        setTimeout(() => {
            if (alertDiv.parentNode) {
                alertDiv.remove();
            }
        }, duration);
    }

    static getIcon(type) {
        const icons = {
            'success': '<i class="bi bi-check-circle-fill text-success me-2"></i>',
            'danger': '<i class="bi bi-exclamation-triangle-fill text-danger me-2"></i>',
            'warning': '<i class="bi bi-exclamation-circle-fill text-warning me-2"></i>',
            'info': '<i class="bi bi-info-circle-fill text-info me-2"></i>'
        };
        return icons[type] || '';
    }

    static success(message, duration) {
        this.show('success', message, duration);
    }

    static error(message, duration) {
        this.show('danger', message, duration);
    }

    static warning(message, duration) {
        this.show('warning', message, duration);
    }

    static info(message, duration) {
        this.show('info', message, duration);
    }
}

// Global loading manager'ı başlat
const loadingManager = new LoadingManager();

// Global fonksiyonlar
window.showLoading = () => loadingManager.show();
window.hideLoading = () => loadingManager.hide();
window.showAlert = (type, message, duration) => AlertManager.show(type, message, duration);
window.showSuccess = (message, duration) => AlertManager.success(message, duration);
window.showError = (message, duration) => AlertManager.error(message, duration);
window.showWarning = (message, duration) => AlertManager.warning(message, duration);
window.showInfo = (message, duration) => AlertManager.info(message, duration); 