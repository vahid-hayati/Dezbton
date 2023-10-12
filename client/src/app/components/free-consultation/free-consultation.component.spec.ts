import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FreeConsultationComponent } from './free-consultation.component';

describe('FreeConsultationComponent', () => {
  let component: FreeConsultationComponent;
  let fixture: ComponentFixture<FreeConsultationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FreeConsultationComponent]
    });
    fixture = TestBed.createComponent(FreeConsultationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
