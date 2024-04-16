import React, { useMemo, useState } from 'react'
import { Pagination, GroupMembersView } from '../../../shared/components/dashboard'
import "./TeachersPageView.css"
import { setOrganizationTeachers } from '../../../shared/network/organization';
import useOrganizationTeachers from './useOrganizationTeachers';

const itemsPerPage = 10;

const TeachersPageView = () => {
    const [Teachers, setTeachers] = useOrganizationTeachers();
    const [currentPage, setCurrentPage] = useState(1);

    const highestPage = useMemo(() => {
        return Math.ceil(Teachers?.length / itemsPerPage);
    }, [Teachers])
    const updateTeachers = (newTeachers) => {
        setTeachers(newTeachers);
        setOrganizationTeachers(newTeachers);
    }
    const updateCurrentPage = (newCurrentPage) => {
        setCurrentPage(newCurrentPage)
    }

  return (
    <div className='TeachersPageView'>
        <GroupMembersView title="Lärare" highestPage={highestPage} currentPage={currentPage} itemsPerPage={itemsPerPage} groupMembers={Teachers} setGroupMembers={updateTeachers}></GroupMembersView>
        <Pagination highestPage={highestPage} currentPage={currentPage} setCurrentPage={updateCurrentPage} itemsPerPage={itemsPerPage} itemCount={Teachers?.length}></Pagination>
    </div>
  )
}

export default TeachersPageView