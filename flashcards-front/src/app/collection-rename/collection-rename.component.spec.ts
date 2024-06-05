import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionRenameComponent } from './collection-rename.component';

describe('CollectionRenameComponent', () => {
  let component: CollectionRenameComponent;
  let fixture: ComponentFixture<CollectionRenameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CollectionRenameComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CollectionRenameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
