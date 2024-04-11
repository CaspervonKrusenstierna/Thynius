import React, { useState } from 'react'
import nONs from './components/nONs/nONs'
import SubmittmentsContainer from './components/submittmentscontainer/SubmittmentsContainer'
import { Pagination } from '../../../../../../shared/components/dashboard'
import useNotSubmittedUsers from '../hooks/useNotSubmittedUsers'
import useNonSubmittmentFilter from './hooks/useNonSubmittmentFilter'
import NonSubmittmentsHeader from './nonsubmittmentsheader/NonSubmittmentsHeader'
import NonSubmittmentsContainer from './nonsubmittmentscontainer/NonSubmittmentsContainer'


const itemsPerPage = 15;
const NonSubmittmentsView = (props) => {
    const [displaySettings, setDisplaySettings] = useState({search: ""});
    const [currentPage, setCurrentPage] = useState(1);
    const nonSubmittments = useNotSubmittedUsers(props.assignmentId);
    const filteredNonSubmittments = useNonSubmittmentFilter(nonSubmittments, displaySettings, itemsPerPage);
    
    function setSearch(search){
        setCurrentPage(1);
        let temp = displaySettings;
        setDisplaySettings({filter: temp.filter, search: search})
    }

    return (
        <>
            <NonSubmittmentsHeader setSearch={(search) => {setSearch(search)}}></NonSubmittmentsHeader>
            <NonSubmittmentsContainer currentPage={currentPage} displaySettings={displaySettings} itemsPerPage={itemsPerPage} nonSubmittments={filteredSubmittments.pages}></NonSubmittmentsContainer>
            <Pagination highestPage={filteredSubmittments.dataEndPage} currentPage={currentPage} setCurrentPage={setCurrentPage} itemsPerPage={itemsPerPage} itemCount={filteredSubmittments.pages.length} ></Pagination>
        </>
    )
}

export default NonSubmittmentsView