import React, { useState } from 'react'
import SubmittmentsHeader from './components/submittmentsheader/SubmittmentsHeader'
import SubmittmentsContainer from './components/submittmentscontainer/SubmittmentsContainer'
import useFetchPaginatedData from '../../../hooks/useFetchPaginatedData'
import useSubmittmentFilter from './useSubmittmentFilter'
import Pagination from '../pagination/Pagination'


const itemsPerPage = 15;
const SubmittmentsView = (props) => {
    const [displaySettings, setDisplaySettings] = useState({filter: 0, search: ""});
    const [currentPage, setCurrentPage] = useState(1);
    const paginatedData = useFetchPaginatedData("/assignment/usertexts?assignmentId=" + props.assignmentId, currentPage, 10);
    const filteredPaginatedData = useSubmittmentFilter(paginatedData, displaySettings, itemsPerPage);
    
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
        <>
            <SubmittmentsHeader setFilter={(filter) => {setFilter(filter)}} setSearch={(search) => {setSearch(search)}}></SubmittmentsHeader>
            <SubmittmentsContainer currentPage={currentPage} displaySettings={displaySettings} itemsPerPage={itemsPerPage} submittments={filteredPaginatedData.pages}></SubmittmentsContainer>
            <Pagination highestPage={filteredPaginatedData.dataEndPage} currentPage={currentPage} setCurrentPage={setCurrentPage} itemsPerPage={itemsPerPage} itemCount={filteredPaginatedData.pages.length} ></Pagination>
        </>
    )
}

export default SubmittmentsView