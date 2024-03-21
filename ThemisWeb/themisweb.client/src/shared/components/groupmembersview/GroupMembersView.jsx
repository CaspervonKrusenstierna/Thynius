import React, { useState } from 'react'
import "./GroupMembersView.css"
import UserSearch from '../usersearch/UserSearch'
import GroupMemberContainer from './groupmembercontainer/GroupMemberContainer'

const GroupMembersView = (props) => {
  const [groupMembers, setGroupMembers] = useState([]);
  function onAddMember(Member){
    let memberAlreadyInList = false;
    for(let i = 0; groupMembers.length > i; i++){
        if(groupMembers[i].username == Member.username){
            memberAlreadyInList = true;
        }
    } 
    if(!memberAlreadyInList){
        let temp = groupMembers.slice(); 
        temp.push(Member);
        props.setGroupMembers(temp);
        setGroupMembers(temp);
    }
  }
  function onDeleteMember(Member){
    for(let i = 0; groupMembers.length > i; i++){
        if(groupMembers[i] == Member){
            let temp = groupMembers;
            temp = temp.splice(i, i);
            props.setGroupMembers(temp);
            setGroupMembers(temp);
        }
    }
  }
  return (
    <div className='GroupMembersView'>
        <p className='GroupMembersView-Title'>Medlemmar</p>
        <UserSearch onSubmit={(submission) => {onAddMember(submission)}}></UserSearch>
        <GroupMemberContainer members={groupMembers} onMemberDelete={onDeleteMember}></GroupMemberContainer>
    </div>
  )
}

export default GroupMembersView