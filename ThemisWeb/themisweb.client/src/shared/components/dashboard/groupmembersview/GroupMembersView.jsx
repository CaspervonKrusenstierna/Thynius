import React, { useMemo, useState } from 'react'
import "./GroupMembersView.css"
import UserSearch from '../usersearch/UserSearch'
import GroupMemberContainer from './groupmembercontainer/GroupMemberContainer'

const GroupMembersView = (props) => {
  function onAddMember(Member){
    let memberAlreadyInList = false;
    for(let i = 0; props.groupMembers.length > i; i++){
        if(props.groupMembers[i].username == Member.username){
            memberAlreadyInList = true;
        }
    } 
    if(!memberAlreadyInList){
        let temp = props.groupMembers.slice(); 
        temp.push(Member);
        props.setGroupMembers(temp);
    }
  }
  function onDeleteMember(Member){
    for(let i = 0; props.groupMembers.length > i; i++){
        if(props.groupMembers[i].username == Member.username){
            props.setGroupMembers([...props.groupMembers.slice(0,i), ...props.groupMembers.slice(i + 1)]);
        }
    }
  }
  const currentPageMembers = useMemo(() => {
    if(props.highestPage == props.currentPage){
      return props.groupMembers.slice((props.currentPage-1)*10)
    }
    else{
      return props.groupMembers.slice((props.currentPage-1)*10, props.currentPage*10)
    }
  }, [props.groupMembers, props.currentPage, props.itemsPerPage])

  return (
    <div className='GroupMembersView'>
      <div className='GroupMembersView-Header'>
        <p className='GroupMembersView-Title'>Medlemmar</p>
        <UserSearch onSubmit={(submission) => {onAddMember(submission)}}></UserSearch>
      </div>
        <GroupMemberContainer members={currentPageMembers} onMemberDelete={onDeleteMember}></GroupMemberContainer>
    </div>
  )
}

export default GroupMembersView