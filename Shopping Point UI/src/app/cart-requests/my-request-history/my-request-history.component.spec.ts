import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRequestHistoryComponent } from './my-request-history.component';

describe('MyRequestHistoryComponent', () => {
  let component: MyRequestHistoryComponent;
  let fixture: ComponentFixture<MyRequestHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyRequestHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyRequestHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
