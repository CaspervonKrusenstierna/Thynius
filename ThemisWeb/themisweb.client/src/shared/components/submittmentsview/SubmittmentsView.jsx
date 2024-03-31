import React, {useState } from 'react'
import SubmittmentsHeader from './components/submittmentsheader/SubmittmentsHeader'
import SubmittmentsContainer from './components/submittmentscontainer/SubmittmentsContainer'

const SubmittmentsView = (props) => {
    const [displaySettings, setDisplaySettings] = useState({filter: null, search: ""});
    
    function setFilter(filter){
        setDisplaySettings({filter: filter, search: displaySettings.search});
    }
    function setSearch(search){
        setDisplaySettings({filter: displaySettings.filter, search: search})
    }

    return (
        <>
            <SubmittmentsHeader setFilter={setFilter} setSearch={setSearch}></SubmittmentsHeader>
            <SubmittmentsContainer displaySettings={displaySettings} submittments={props.submittments}></SubmittmentsContainer>
        </>
    )
}

export default SubmittmentsView