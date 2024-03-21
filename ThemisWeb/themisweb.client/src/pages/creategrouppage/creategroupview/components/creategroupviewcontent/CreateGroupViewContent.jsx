import React, { useCallback, useRef, useState } from 'react'
import "./CreateGroupViewContent.css"
import VerticalInput from '../../../../../shared/components/verticalinput/VerticalInput'
import { ErrorMessage, SubmitButton } from '../../../../../shared/components'
import CreateGroupSubmitButton from './components/creategroupsubmitbutton/CreateGroupSubmitButton'
import useFetch from '../../../../../shared/hooks/useFetch'
import { useNavigate } from 'react-router-dom'
import GroupMembersView from '../../../../../shared/components/groupmembersview/GroupMembersView'

const CreateGroupViewContent = () => {
    const [errorMessage, setErrorMessage] = useState();

    const groupToCreateInfo = useRef({groupName: "", groupMemberIds: []})

    const navigator = useNavigate();

    onSubmitClick = async () => {
        const response = await useFetch("/group", "POST", groupToCreateInfo.current);
        if(response.ok){
            navigator("/dashboard/groups");
        }
        response.json().then(s => {setErrorMessage(s.Title)});
    };

    const updateGroupMembers = (GroupMembers) => {
        groupToCreateInfo.current.groupMemberIds = GroupMembers.map(GroupMember => GroupMember.id);
    }
    const updateGroupName = (groupName) => {
        groupToCreateInfo.current.groupName = groupName;
    }

  return (
    <div className='CreateGroupViewContent'>
        <div className='CreateGroupViewContent-TopContainer'>
            {errorMessage ? <ErrorMessage message={errorMessage}></ErrorMessage> : <></>}
            <VerticalInput onChange={(e) => {updateGroupName(e.target.value)}}Title="Namn"></VerticalInput>
            <GroupMembersView setGroupMembers={updateGroupMembers}></GroupMembersView>
        </div>
        <div className='CreateGroupViewContent-BottomContainer'>
            <CreateGroupSubmitButton onClick={cachedOnSubmitClick}></CreateGroupSubmitButton>
        </div>
    </div>
  )
}

export default CreateGroupViewContent