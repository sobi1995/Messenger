import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import 'hammerjs';
import { SharedModule } from './core/modules/shared.module';
import { AppComponent } from './app.component';
import { FuseSplashScreenService } from './core/services/splash-screen.service';
import { FuseConfigService } from './core/services/config.service';
import { FuseNavigationService } from './core/components/navigation/navigation.service';  
import { TranslateModule } from '@ngx-translate/core';
import { SobhanModule } from './main/content/apps/sobhan/sobhan.module';
 
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { FuseFakeDbService } from './fuse-fake-db/fuse-fake-db.service';
import { AboutComponent } from './main/content/about/about.component';
import { ToastModule } from 'ng2-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './core/modules/material.module';
import { PublicChatModule } from './main/content/apps/public-chat/public-chat.module';
 
 
import { BaselayoutComponent } from './core/components/layout/baselayout/baselayout.component';
import { LoginlayoutComponent } from './core/components/layout/loginlayout/loginlayout.component';
 
import { FuseThemeOptionsComponent } from './core/components/theme-options/theme-options.component';
 
import { HomeComponent } from './main/content/home/home.component';
import { FuseNavigationModule } from './core/components/navigation/navigation.module';
import { FuseShortcutsModule } from './core/components/shortcuts/shortcuts.module';
import { FuseSearchBarModule } from './core/components/search-bar/search-bar.module';
 
import { LoginModule } from './Module/login/login.module';
import { FuseChatModule } from './main/content/apps/chat/chat.module';
import { FuseContentComponent } from './core/components/layout/user-panellayout/content.component';
import { FuseMainComponent } from './core/components/layout/user-panellayout/main.component';
import { FuseToolbarComponent } from './core/components/layout/user-panellayout/toolbar/toolbar.component';
import { FuseFooterComponent } from './core/components/layout/user-panellayout/footer/footer.component';
import { FuseQuickPanelComponent } from './core/components/layout/user-panellayout/quick-panel/quick-panel.component';
 
 

 


 
export const routes: Routes = [
    
    { 
                path: '', 
                component: BaselayoutComponent,
                children: [
                  { path: '', component: HomeComponent, pathMatch: 'full'},
                 
                ]
            },
            { 
                path: '', 
                component: LoginlayoutComponent,
                children: [
                  { path: 'َAbout', component: AboutComponent, pathMatch: 'full'},
                 
                ]
            },
          
  ];
  

@NgModule({
    declarations: [
        AppComponent,
          LoginlayoutComponent,
          BaselayoutComponent,
        
       FuseContentComponent,
        FuseFooterComponent,
        FuseMainComponent,
        FuseToolbarComponent,
        FuseThemeOptionsComponent,
        FuseQuickPanelComponent,
        AboutComponent,
        HomeComponent,
     
       
     
        
    ],
    imports     : [
        RouterModule.forRoot(routes) ,
        RouterModule.forChild(routes) ,
    //    ToastModule.forRoot(),
        BrowserModule,
        HttpClientModule,
        BrowserAnimationsModule,
        BrowserModule,
        LoginModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        SharedModule,
        TranslateModule.forRoot(),
        SobhanModule,
        FuseChatModule,
        PublicChatModule,
        InMemoryWebApiModule.forRoot(FuseFakeDbService, {
            delay             : 0,
            passThruUnknownUrl: true
        }),
       
                FuseNavigationModule,
                FuseShortcutsModule,
                FuseSearchBarModule,
    ],
    providers   : [
        FuseSplashScreenService,
        FuseConfigService,
        FuseNavigationService
    ],
    bootstrap   : [
        AppComponent
    ],
    exports:[
        LoginlayoutComponent,
        BaselayoutComponent,
        FuseMainComponent,
        FuseContentComponent,
        FuseFooterComponent,
        FuseMainComponent,
        FuseToolbarComponent,
        FuseThemeOptionsComponent,
        FuseQuickPanelComponent,
    ]
})
export class AppModule
{
}
