import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatCardModule} from '@angular/material/card';


// Components
import { AdminComponent } from './components/admin/admin.component';
import { FeedBackComponent } from './components/feed-back/feed-back.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register-users/register.component';
import { LoginComponent } from './components/login/login.component';
import { ListUsersComponent } from './components/list-users/list-users.component';
import { UpdateComponent } from './components/update/update.component';
import { ProductsComponent } from './components/products/products.component';
import { ComparisonComponent } from './components/comparison/comparison.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { ContactUsComponent } from './components/contact-us/contact-us.component';
import { CompletedProjectsComponent } from './components/completed-projects/completed-projects.component';
import { FreeConsultationComponent } from './components/free-consultation/free-consultation.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    FeedBackComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ListUsersComponent,
    UpdateComponent,
    ProductsComponent,
    ComparisonComponent,
    AboutUsComponent,
    ContactUsComponent,
    CompletedProjectsComponent,
    FreeConsultationComponent,
    NotFoundComponent,
    FooterComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
