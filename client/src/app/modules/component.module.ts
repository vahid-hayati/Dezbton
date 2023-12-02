import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutUsComponent } from '../components/about-us/about-us.component';
import { AdminComponent } from '../components/admin/admin.component';
import { ComparisonComponent } from '../components/comparison/comparison.component';
import { CompletedProjectsComponent } from '../components/completed-projects/completed-projects.component';
import { ContactUsComponent } from '../components/contact-us/contact-us.component';
import { FeedBackComponent } from '../components/feed-back/feed-back.component';
import { FooterComponent } from '../components/footer/footer.component';
import { FreeConsultationComponent } from '../components/free-consultation/free-consultation.component';
import { HomeComponent } from '../components/home/home.component';
import { ListUsersComponent } from '../components/list-users/list-users.component';
import { LoginComponent } from '../components/login/login.component';
import { NavbarComponent } from '../components/navbar/navbar.component';
import { NotFoundComponent } from '../components/not-found/not-found.component';
import { ProductsComponent } from '../components/products/products.component';
import { RegisterComponent } from '../components/register-users/register.component';
import { UpdateComponent } from '../components/update/update.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from '../app-routing.module';
import { MaterialModule } from './material.module';

const components = [
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
];

@NgModule({
  declarations: [
    components
  ],
  imports: [
    CommonModule,

    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule
  ],
  exports: [
    components
  ]
})
export class ComponentModule {
  constructor() { }
}