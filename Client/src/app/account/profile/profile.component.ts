import { AccountService } from 'src/app/account/account.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { IUser } from 'src/app/shared/models/user';
import { Observable } from 'rxjs/internal/Observable';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  currentUser$: Observable<IUser>;
  modalRef?: BsModalRef;
  changeImageForm: FormGroup;

  constructor(private accountService: AccountService, private modalService: BsModalService, public fb: FormBuilder, private http: HttpClient) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
    // this.createImageForm();
  }

  // createImageForm() {

  //   this.changeImageForm = new FormGroup({
  //     id: new FormControl(this.accountService.getCurrentUserValue().userId, [Validators.required]),
  //     image: new FormControl('', [Validators.required])
  //   });
  // }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  // uploadFile(event) {
  //   const file = (event.target as HTMLInputElement).files[0];
  //   this.changeImageForm.patchValue({
  //     avatar: file
  //   });
  //   this.changeImageForm.get('image').updateValueAndValidity()
  // }

  // onSubmit() {

  //   var formData: any = new FormData();
  //   console.log(this.changeImageForm.value);
  //   formData.append("id", this.changeImageForm.get('id').value);
  //   formData.append("image", this.changeImageForm.get('image').value);
  //   console.log(formData);
  //   this.http
  //     .post('https://localhost:7272/api/account', formData)
  //     .subscribe({
  //       next: (response) => console.log(response),
  //       error: (error) => console.log(error),
  //     });
  // }

  uploadFile(files) {

    if (files.length == 0) {

      return;
    }
    let fileToUpload = files[0]
    var formData: FormData = new FormData();
    formData.append('id', this.accountService.getCurrentUserValue().userId);
    formData.append('image', fileToUpload, fileToUpload.name);
    console.log(formData)
    this.http
      .post('https://localhost:7272/api/account', formData)
      .subscribe({
        next: (response) => console.log(response),
        error: (error) => console.log(error),
      });
  }

}
