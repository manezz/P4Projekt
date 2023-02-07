import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from '../app.component';
import { AuthService } from '../_services/auth.service';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-headerLoggedIn',
  template: `
    <nav>
      <div class="nav">
        <img id="logo" class="linkLeft" src="/assets/images/socialmachine.png"  routerLink="/main">
        <a class="linkRight" [routerLink]="['/']" (click)="logOut()">Logout</a>
        <a class="linkRight" [routerLink]="['/profile', this.currentUser.loginResponse.user.userId]" >Profile</a>
        <a class="linkRight" id="createbtn" [routerLink]="['/createpost']" >+</a>
      </div>
    </nav>
  `,
  styles: [`
  .nav{
    background-color: gray;
  }
  #logo{
    display: inline-block;
    width: 40px;
    margin-right: 10px;
  }
  #logo:hover{
    cursor: pointer;
  }
  a:link, a:visited{
    display: inline-block;
    border-radius: 5px;
    padding: 5px 5px 5px 5px;
    color: #eee;
    text-decoration: none;
  }
  .linkLeft{
    margin: 0px 5px 0px 5px;
    float: left;
  }
  .linkRight{
    margin: 0px 5px 0px 5px;
    float: right;
  }
  #createbtn{
    font-size: 22px;
    padding: 2px 8px 2px 8px ;
  }
  `]
})
export class HeaderLoggedInComponent {
  
  constructor(private auth: AuthService,  private router: Router, private route: ActivatedRoute, private AppComponent: AppComponent) { 
    this.auth.currentUser.subscribe(x => this.currentUser = x )
  }
  currentUser: any
  user: User[] = []  

  logOut(){
    this.auth.logout()
    this.auth.currentUser.subscribe(x => this.currentUser = x );
    
    //ændrer headeren
    this.AppComponent.validateHeader()

  }

}
