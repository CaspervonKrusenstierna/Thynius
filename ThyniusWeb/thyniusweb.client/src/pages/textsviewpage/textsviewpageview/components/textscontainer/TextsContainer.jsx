import React from 'react'
import useTextsInfo from './hooks/useTextsInfo';
import useDynamicTextsRowCount from './hooks/useDynamicTextsRowCount';
import TextView from "./components/textview/TextView";
import "./TextsContainer.css"
import { RowedItemsContainer } from '../../../../../shared/components/dashboard';
import getTextSkeletonItems from './utils/getTextSkeletonItems';


const TextsContainer = () => {
    const textsInfo = useTextsInfo();
    const textsRowCount = useDynamicTextsRowCount();
    return (
        <div className='TextsContainer'>
            {textsInfo ? 
            <RowedItemsContainer ItemsPerRow={textsRowCount} Items={textsInfo.toReversed().map(j =><TextView Id={j.Id} Title={j.Title}></TextView>)} Filler={<div className="TextFiller"></div>}></RowedItemsContainer>
            : 
            <RowedItemsContainer ItemsPerRow={textsRowCount} Items={getTextSkeletonItems(textsRowCount)}></RowedItemsContainer>
        }
        </div>
    )
}

export default TextsContainer