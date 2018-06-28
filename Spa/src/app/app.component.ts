import { Component, ViewContainerRef } from '@angular/core';
import { FuseSplashScreenService } from './core/services/splash-screen.service';
import { TranslateService } from '@ngx-translate/core';
import { FuseTranslationLoaderService } from './core/services/translation-loader.service';
import { FuseNavigationService } from './core/components/navigation/navigation.service';
import { FuseNavigationModel } from './navigation/navigation.model';
import { locale as navigationEnglish } from './navigation/i18n/en';
import { locale as navigationTurkish } from './navigation/i18n/tr';
import { ToastsManager } from 'ng2-toastr';

@Component({
    selector   : 'fuse-root',
    templateUrl: './app.component.html',
    styleUrls  : ['./app.component.scss']
})
export class AppComponent
{
    constructor(
        private fuseNavigationService: FuseNavigationService,
        private fuseSplashScreen: FuseSplashScreenService,
        private translate: TranslateService,
        private translationLoader: FuseTranslationLoaderService,
        public toastr: ToastsManager, vcr: ViewContainerRef
    )
    {
      this.toastr.setRootViewContainerRef(vcr);
        // Add languages
        this.translate.addLangs(['en', 'tr']);

        // Set the default language
        this.translate.setDefaultLang('en');

        // Use a language
        this.translate.use('en');

        // Set the navigation model
        this.fuseNavigationService.setNavigationModel(new FuseNavigationModel());

        // Set the navigation translations
        this.translationLoader.loadTranslations(navigationEnglish, navigationTurkish);
    }
    showSuccess() {
        this.toastr.success('You are awesome!', 'Success!');
      }
     
      showError() {
      
        this.toastr.error('This is not good!', 'Oops!');
      }
     
      showWarning() {
        this.toastr.warning('You are being warned.', 'Alert!');
      }
     
      showInfo() {
        this.toastr.info('Just some information for you.');
      }
      
      showCustom() {
        this.toastr.custom('<span style="color: red">Message in red.</span>', null, {enableHTML: true});
      }
}
