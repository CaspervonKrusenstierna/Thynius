import React, { useRef } from 'react'
import GroupMembersView from '../../../../../../shared/components/groupmembersview/GroupMembersView'
import "./CreateGroupMembersPage.css"

const CreateGroupMembersPage = (props) => {

    const groupInfo = useRef({groupName: "", groupMembers: []});
    const updateGroupMembers = (newGroupMembers) => {
        groupInfo.current.groupMembers = newGroupMembers.map(s => s.id);
    }
    return (
        <div className='CreateGroupMembersPage'>
            <GroupMembersView setGroupMembers={updateGroupMembers}></GroupMembersView>
        </div>
    )
}

export default CreateGroupMembersPage