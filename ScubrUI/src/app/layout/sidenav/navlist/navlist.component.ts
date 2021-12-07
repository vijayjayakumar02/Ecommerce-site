import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navlist',
  templateUrl: './navlist.component.html',
  styleUrls: ['./navlist.component.scss']
})
export class NavlistComponent implements OnInit {

  products: string[] = ['Laptop', 'Mobile', 'Costumes', 'Electronics'];

  constructor() { }

  ngOnInit(): void {
  }

}
