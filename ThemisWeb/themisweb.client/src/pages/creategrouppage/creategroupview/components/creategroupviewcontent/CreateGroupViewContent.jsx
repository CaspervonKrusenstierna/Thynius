import React, { useCallback, useRef, useState } from 'react'
import "./CreateGroupViewContent.css"
import VerticalInput from '../../../../../shared/components/verticalinput/VerticalInput'
import { ErrorMessage, SubmitButton } from '../../../../../shared/components'
import CreateGroupSubmitButton from './creategroupsubmitbutton/CreateGroupSubmitButton'
import useFetch from '../../../../../shared/hooks/useFetch'
import { useNavigate } from 'react-router-dom'
import GroupMembersView from '../../../../../shared/components/groupmembersview/GroupMembersView'

const CreateGroupViewContent = () => {
    const [errorMessage, setErrorMessage] = useState();
    const groupInfo = useRef({groupName: "", groupMembers: []});
    const navigator = useNavigate();
    const onSubmitClick = async () =>{
        const response = await useFetch("/group?GroupName="+groupInfo.current.groupName, "POST", groupInfo.current.groupMembers);
        if(response.ok){
            navigator("/dashboard/groups");
        }
        response.json().then(s => {setErrorMessage(s.Title)});
    }
    const updateGroupMembers = (newGroupMembers) => {
        groupInfo.current.groupMembers = newGroupMembers.map(s => s.id);
    }
    const updateGroupName = (newGroupName) => {
        groupInfo.current.groupName = newGroupName;
    }

  return (
    <div className='CreateGroupViewContent'>
        <div className='CreateGroupViewContent-TopContainer'>
            {errorMessage ? <ErrorMessage message={errorMessage}></ErrorMessage> : <></>}
            <VerticalInput onChange={(e) => {updateGroupName(e.target.value)}}Title="Namn"></VerticalInput>
            <GroupMembersView setGroupMembers={updateGroupMembers}></GroupMembersView>
        </div>
        <div className='CreateGroupViewContent-BottomContainer'>
            <CreateGroupSubmitButton onClick={onSubmitClick}></CreateGroupSubmitButton>
        </div>
    </div>
  )
}

export default CreateGroupViewContent