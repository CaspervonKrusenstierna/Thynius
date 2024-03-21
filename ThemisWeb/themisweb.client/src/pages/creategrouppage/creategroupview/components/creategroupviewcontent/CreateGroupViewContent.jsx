import React, { useCallback, useRef, useState } from 'react'
import "./CreateGroupViewContent.css"
import VerticalInput from '../../../../../shared/components/verticalinput/VerticalInput'
import { ErrorMessage, SubmitButton } from '../../../../../shared/components'
import CreateGroupSubmitButton from './components/creategroupsubmitbutton/CreateGroupSubmitButton'
import useFetch from '../../../../../shared/hooks/useFetch'
import { useNavigate } from 'react-router-dom'
import GroupMembersView from '../../../../../shared/components/groupmembersview/GroupMembersView'

const CreateGroupViewContent = () => {
    const groupName = useRef("");
    const [errorMessage, setErrorMessage] = useState();
    const groupMemberIds = useRef([]);
    const navigator = useNavigate();
    const cachedOnSubmitClick = useCallback(async () => {
        const response = await useFetch("/group", "POST", JSON.stringify({GroupData: ({GroupName: groupName.current, UserIds: groupMemberIds.current})}));
        if(response.ok){
            navigator("/dashboard/groups");
        }
        response.json().then(s => {setErrorMessage(s.Title)});
    }, [groupName.Val])

    const updateGroupMembers = (GroupMembers) => {
        let temp = [];
        for(let i = 0; GroupMembers.length > i; i++){
            temp.push(GroupMembers[i].id);
        }
        groupMemberIds.current = temp;
    }

  return (
    <div className='CreateGroupViewContent'>
        <div className='CreateGroupViewContent-TopContainer'>
            {errorMessage ? <ErrorMessage message={errorMessage}></ErrorMessage> : <></>}
            <VerticalInput onChange={(e) => {groupName.current=e.target.value}}Title="Namn"></VerticalInput>
            <GroupMembersView setGroupMembers={updateGroupMembers}></GroupMembersView>
        </div>
        <div className='CreateGroupViewContent-BottomContainer'>
            <CreateGroupSubmitButton onClick={cachedOnSubmitClick}></CreateGroupSubmitButton>
        </div>
    </div>
  )
}

export default CreateGroupViewContent