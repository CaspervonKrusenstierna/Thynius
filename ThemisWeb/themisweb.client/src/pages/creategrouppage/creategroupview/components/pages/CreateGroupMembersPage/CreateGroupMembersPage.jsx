import React, { useContext, useMemo, useState } from 'react'
import "./CreateGroupMembersPage.css"
import { createGroupContext } from '../../../CreateGroupView'
import { GroupMembersView, Pagination } from '../../../../../../shared/components/dashboard'

const itemsPerPage = 10;

const CreateGroupMembersPage = (props) => {
    const createGroupInfo = useContext(createGroupContext);
    const [groupMembers, setGroupMembers] = useState(createGroupInfo.current.groupMembers);
    const [currentPage, setCurrentPage] = useState(createGroupInfo.current.currentMembersPage);
    const highestPage = useMemo(() => {
        return Math.ceil(groupMembers.length / itemsPerPage);
    }, [groupMembers])
    const updateGroupMembers = (newGroupMembers) => {
        createGroupInfo.current.groupMembers = newGroupMembers;
        setGroupMembers(newGroupMembers);
    }
    const updateCurrentPage = (newCurrentPage) => {
        createGroupInfo.current.currentMembersPage = newCurrentPage;
        setCurrentPage(newCurrentPage)
    }

    return (
        <div className='CreateGroupMembersPage'>
            <GroupMembersView highestPage={highestPage} currentPage={currentPage} itemsPerPage={itemsPerPage} groupMembers={createGroupInfo.current.groupMembers} setGroupMembers={updateGroupMembers}></GroupMembersView>
            <Pagination highestPage={highestPage} currentPage={currentPage} setCurrentPage={updateCurrentPage} itemsPerPage={itemsPerPage} itemCount={groupMembers.length}></Pagination>
        </div>
    )
}

export default CreateGroupMembersPage