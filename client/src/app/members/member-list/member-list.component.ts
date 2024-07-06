import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member.model';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent implements OnInit {
  private memberService = inject(MembersService);
  members: Member[] = [];
  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    return this.memberService.getMembers().subscribe({
      next: (members) => (this.members = members),
    });
  }
}
