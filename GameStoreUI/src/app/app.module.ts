import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './users/register/components/register.component';
import { LoginComponent } from './users/login/components/login.component';
import { MainPageComponent } from './gamePages/components/main-page/main-page.component';
import { GamePageComponent } from './gamePages/components/game-page/game-page.component';
import { GameGenreComponent } from './gamePages/components/game-genre/game-genre.component';
import { MenuComponent } from './menu/components/menu/menu.component';
import { GameCompanyComponent } from './gamePages/components/game-company/game-company.component';
import { GameActionComponent } from './adminPages/game-actions/components/game-creation/game-creation.component';
import { GenreActionComponent } from './adminPages/genre-actions/components/genre-creation/genre-creation.component';
import { CompanyActionsComponent } from './adminPages/company-actions/components/company-creation/company-creation.component';
import { GenreEditingComponent } from './adminPages/genre-actions/components/genre-editing/genre-editing/genre-editing.component';
import { AdminMainPageComponent } from './adminPages/main-adminPage/components/main-page/main-page/main-page.component';
import { GameEditingComponent } from './adminPages/game-actions/components/game-editing/game-editing.component';
import { GenreMainPageComponent } from './adminPages/genre-actions/components/genre-main-page/genre-main-page.component';
import { CompanyEditingComponent } from './adminPages/company-actions/components/company-editing/company-editing.component';
import { CompanyMainPageComponent } from './adminPages/company-actions/components/company-main-page/company-main-page.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SearchComponent } from './menu/components/search/search.component';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './authorization/services/authentication.service';
import { AuthGuard } from './authorization/services/auth.guard';
import { CartComponent } from './users/userPages/components/cart/cart.component';
import { ShoppingHistoryComponent } from './users/userPages/components/shopping-history/shopping-history.component';
import { SettingsComponent } from './users/userPages/components/settings/settings.component';
import { PasswordChangeComponent } from './users/userPages/components/settings/password-change/password-change.component';
import { InterceptorsModule } from '../infrastructure/interceptors/interceptors.module';
import { ToastrModule } from 'ngx-toastr';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserContext } from './users/current-user-data/models/user-context.model';
import { ClientChatComponent } from './users/client-chat/components/client-chat/client-chat.component';
import { AdminChatMainPageComponent } from './adminPages/chat-actions/components/adminChatMainPage/admin-chat-mainPage.component';
import { AdminChatPageComponent } from './adminPages/chat-actions/components/admin-chat-page/admin-chat-page.component';
import { RestorePasswordComponent } from './users/restore-password/restore-password.component';
import { NameSymbolValidatorDirective } from '../infrastructure/validators/name-symbol-validator.directive';

@NgModule({
  declarations: [
    AdminMainPageComponent,
    AppComponent,
    RegisterComponent,
    LoginComponent,
    MainPageComponent,
    GamePageComponent,
    GameGenreComponent,
    MenuComponent,
    GameCompanyComponent,
    GameActionComponent,
    GenreActionComponent,
    CompanyActionsComponent,
    GenreEditingComponent,
    GameEditingComponent,
    GenreMainPageComponent,
    CompanyEditingComponent,
    CompanyMainPageComponent,
    SearchComponent,
    CartComponent,
    ShoppingHistoryComponent,
    SettingsComponent,
    PasswordChangeComponent,
    AdminChatMainPageComponent,
    ClientChatComponent,
    AdminChatPageComponent,
    RestorePasswordComponent,
    NameSymbolValidatorDirective
  ],
  exports:[
    NameSymbolValidatorDirective
  ],
  imports: [
    NgMultiSelectDropDownModule.forRoot(),
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    AuthenticationService, 
    AuthGuard, 
    UserContext,
    InterceptorsModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

