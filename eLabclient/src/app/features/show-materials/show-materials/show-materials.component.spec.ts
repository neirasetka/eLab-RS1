import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowMaterialsComponent } from './show-materials.component';

describe('ShowMaterialsComponent', () => {
  let component: ShowMaterialsComponent;
  let fixture: ComponentFixture<ShowMaterialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowMaterialsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowMaterialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
