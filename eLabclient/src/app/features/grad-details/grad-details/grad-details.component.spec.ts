import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradDetailsComponent } from './grad-details.component';

describe('GradDetailsComponent', () => {
  let component: GradDetailsComponent;
  let fixture: ComponentFixture<GradDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GradDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
