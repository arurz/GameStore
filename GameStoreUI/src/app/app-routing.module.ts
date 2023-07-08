import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './users/register/components/register.component';
import { LoginComponent } from './users/login/components/login.component';
import { MainPageComponent } from './gamePages/components/main-page/main-page.component';
import { GamePageComponent } from './gamePages/components/game-page/game-page.component';
import { GameGenreComponent } from './gamePages/components/game-genre/game-genre.component';
import { GameCompanyComponent } from './gamePages/components/game-company/game-company.component';
import { GameActionComponent } from './adminPages/game-actions/components/game-creation/game-creation.component';
import { GenreActionComponent } from './adminPages/genre-actions/components/genre-creation/genre-creation.component';
import { CompanyActionsComponent } from './adminPages/company-actions/components/company-creation/company-creation.component';
import { AdminMainPageComponent } from './adminPages/main-adminPage/components/main-page/main-page/main-page.component';
import { GameEditingComponent } from './adminPages/game-actions/components/game-editing/game-editing.component';
import { GenreMainPageComponent } from './adminPages/genre-actions/components/genre-main-page/genre-main-page.component';
import { GenreEditingComponent } from './adminPages/genre-actions/components/genre-editing/genre-editing/genre-editing.component';
import { CompanyMainPageComponent } from './adminPages/company-actions/components/company-main-page/company-main-page.component';
import { CompanyEditingComponent } from './adminPages/company-actions/components/company-editing/company-editing.component';
import { SearchComponent } from './menu/components/search/search.component';
import { AuthGuard } from './authorization/services/auth.guard';
import { CartComponent } from './users/userPages/components/cart/cart.component';
import { ShoppingHistoryComponent } from './users/userPages/components/shopping-history/shopping-history.component';
import { SettingsComponent } from './users/userPages/components/settings/settings.component';
import { PasswordChangeComponent } from './users/userPages/components/settings/password-change/password-change.component';
import { AdminGuard } from './authorization/services/admin.guard';
import { AdminChatMainPageComponent } from './adminPages/chat-actions/components/adminChatMainPage/admin-chat-mainPage.component';
import { AdminChatPageComponent } from './adminPages/chat-actions/components/admin-chat-page/admin-chat-page.component';
import { RestorePasswordComponent } from './users/restore-password/restore-password.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'restorePassword', component: RestorePasswordComponent },
  
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
  { path: 'settings/password', component: PasswordChangeComponent, canActivate: [AuthGuard] },
  { path: 'history', component: ShoppingHistoryComponent, canActivate: [AuthGuard] },

  { path: 'games', component: MainPageComponent },
  { path: 'games/:id', component: GamePageComponent },
  { path: 'admin', component: AdminMainPageComponent, canActivate: [AdminGuard] },
  { path: 'admin/game/details/:id', component: GameEditingComponent, canActivate: [AdminGuard] },
  { path: 'admin/games/create', component: GameActionComponent, canActivate: [AdminGuard] },

  { path: 'admin/genres', component: GenreMainPageComponent, canActivate: [AdminGuard] },
  { path: 'admin/genres/details/:id', component: GenreEditingComponent, canActivate: [AdminGuard] },
  { path: 'admin/genres/create', component: GenreActionComponent, canActivate: [AdminGuard] },

  { path: 'admin/companies', component: CompanyMainPageComponent, canActivate: [AdminGuard] },
  { path: 'admin/companies/create', component: CompanyActionsComponent, canActivate: [AdminGuard] },
  { path: 'admin/companies/details/:id', component: CompanyEditingComponent, canActivate: [AdminGuard] },

  { path: 'admin/chat', component: AdminChatMainPageComponent, canActivate: [AdminGuard] },
  { path: 'admin/chat/:id', component: AdminChatPageComponent, canActivate: [AdminGuard]},

  { path: 'genres/create', component: GenreActionComponent, canActivate: [AdminGuard] },
  { path: 'genregame/gamesByGenre/:id', component: GameGenreComponent },
  { path: 'gamecompany/gameByCompany/:id', component: GameCompanyComponent },
  { path: 'menu/search', component: SearchComponent },

  { path: '**', redirectTo: 'games' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
