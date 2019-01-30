import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StopFinderComponent } from './stop-finder.component';

describe('StopFinderComponent', () => {
  let component: StopFinderComponent;
  let fixture: ComponentFixture<StopFinderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StopFinderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StopFinderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
