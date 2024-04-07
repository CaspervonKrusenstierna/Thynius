import React from 'react'
import useTextsInfo from './hooks/useTextsInfo';
import useDynamicTextsRowCount from './hooks/useDynamicTextsRowCount';
import TextView from "./components/textview/TextView";
import "./TextsContainer.css"
import { RowedItemsContainer } from '../../../../../shared/components/dashboard';

const TextsContainer = () => {
    const textsInfo = useTextsInfo();
    const textsRowCount = useDynamicTextsRowCount();
    return (
        <div className='TextsContainer'>
            {<RowedItemsContainer ItemsPerRow={textsRowCount} Items={textsInfo?.map(j =><TextView Id={j.Id} Title={j.Title}></TextView>)} Filler={<div className="TextFiller"></div>}></RowedItemsContainer>}
        </div>
    )
}

export default TextsContainer