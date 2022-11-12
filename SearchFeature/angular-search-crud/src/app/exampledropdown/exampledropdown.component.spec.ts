import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExampledropdownComponent } from './exampledropdown.component';

describe('ExampledropdownComponent', () => {
  let component: ExampledropdownComponent;
  let fixture: ComponentFixture<ExampledropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExampledropdownComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExampledropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
