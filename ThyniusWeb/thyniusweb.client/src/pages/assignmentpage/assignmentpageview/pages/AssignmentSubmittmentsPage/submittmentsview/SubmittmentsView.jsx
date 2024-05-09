import React, { useState } from 'react'
import SubmittmentsHeader from './components/submittmentsheader/SubmittmentsHeader'
import SubmittmentsContainer from './components/submittmentscontainer/SubmittmentsContainer'
import useSubmittmentFilter from './hooks/useSubmittmentFilter'
import { Pagination } from '../../../../../../shared/components/dashboard'
import useAssignmentSubmittments from "./hooks/useAssignmentSubmittments"

const itemsPerPage = 15;
const SubmittmentsView = (props) => {
    const [displaySettings, setDisplaySettings] = useState({filter: 0, search: ""});
    const [currentPage, setCurrentPage] = useState(1);
    const assignmentSubmittments = useAssignmentSubmittments(props.assignmentId);
    const filteredSubmittments = useSubmittmentFilter(assignmentSubmittments, displaySettings, itemsPerPage);
    
    function setFilter(filter){
        setCurrentPage(1);
        let temp = displaySettings;
        setDisplaySettings({filter: filter, search: temp.search});
    }
    function setSearch(search){
        setCurrentPage(1);
        let temp = displaySettings;
        setDisplaySettings({filter: temp.filter, search: search})
    }

    return (
        <div className='flex flex-col items-center'>
            <SubmittmentsHeader setFilter={(filter) => {setFilter(filter)}} setSearch={(search) => {setSearch(search)}}></SubmittmentsHeader>
            <SubmittmentsContainer currentPage={currentPage} displaySettings={displaySettings} itemsPerPage={itemsPerPage} submittments={filteredSubmittments.pages}></SubmittmentsContainer>
            <Pagination highestPage={filteredSubmittments.dataEndPage} currentPage={currentPage} setCurrentPage={setCurrentPage} itemsPerPage={itemsPerPage} itemCount={filteredSubmittments.pages.length} ></Pagination>
        </div>
    )
}

export default SubmittmentsView