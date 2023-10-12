import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { ComparisonComponent } from './components/comparison/comparison.component';
import { CompletedProjectsComponent } from './components/completed-projects/completed-projects.component';
import { ContactUsComponent } from './components/contact-us/contact-us.component';
import { FreeConsultationComponent } from './components/free-consultation/free-consultation.component';
import { ListUsersComponent } from './components/list-users/list-users.component';
import { LoginComponent } from './components/login/login.component';
import { ProductsComponent } from './components/products/products.component';
import { RegisterComponent } from './components/register-users/register.component';
import { AdminComponent } from './components/admin/admin.component';
import { FeedBackComponent } from './components/feed-back/feed-back.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'مشاوره رایگان', component: FreeConsultationComponent},
  {path: 'ثبت نام', component: RegisterComponent},
  {path: 'ورود', component: LoginComponent},
  {path: 'ادمین',component: AdminComponent},
  {path: 'خانه', component: HomeComponent},
  {path: 'محصولات', component: ProductsComponent},
  {path: 'پروژه های اجرا شده', component: CompletedProjectsComponent},
  {path: 'معرفی شرکت', component: AboutUsComponent},
  {path: 'تماس با ما', component: ContactUsComponent},
  {path: 'مقایسه ما', component: ComparisonComponent},
  {path: 'نظرات شما', component: FeedBackComponent},
  {path: 'لیست کاربران', component: ListUsersComponent},
  {path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
