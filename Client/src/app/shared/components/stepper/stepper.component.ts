import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss']
})
export class StepperComponent extends CdkStepper implements OnInit {

  @Input() linearModeSelected:boolean;

  ngOnInit(): void {
  }

  onClick(index : number)
  {
    this.selectedIndex = index;
  }

}
