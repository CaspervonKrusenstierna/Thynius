import React, { useContext, useMemo, useState } from 'react'
import "./ManageGroupMembersPage.css"
import GroupMembersView from '../groupmembersview/GroupMembersView';
import Pagination from '../pagination/Pagination';

const itemsPerPage = 10;

const ManageGroupMembersPage = (props) => {
    const [groupMembers, setGroupMembers] = useState(props.groupInfo.current.groupMembers);
    const [currentPage, setCurrentPage] = useState(props.groupInfo.current.currentMembersPage);
    const highestPage = useMemo(() => {
        return Math.ceil(groupMembers.length / itemsPerPage);
    }, [groupMembers])
    
    const updateGroupMembers = (newGroupMembers) => {
        props.groupInfo.current.groupMembers = newGroupMembers;
        setGroupMembers(newGroupMembers);
    }
    const updateCurrentPage = (newCurrentPage) => {
        props.groupInfo.current.currentMembersPage = newCurrentPage;
        setCurrentPage(newCurrentPage)
    }
    return (
        <div className='ManageGroupMembersPage'>
            <GroupMembersView highestPage={highestPage} currentPage={currentPage} itemsPerPage={itemsPerPage} groupMembers={props.groupInfo.current.groupMembers} setGroupMembers={updateGroupMembers}></GroupMembersView>
            <Pagination highestPage={highestPage} currentPage={currentPage} setCurrentPage={updateCurrentPage} itemsPerPage={itemsPerPage} itemCount={groupMembers.length}></Pagination>
        </div>
    )
}

export default ManageGroupMembersPage