
import React, { useEffect, useState } from "react";

export default function useDynamicGroupsRowCount() {
    const [itemsPerRow, setItemsPerRow] = useState(0);
    function onResize(){
      let width = window.innerWidth;
      if(width >= 1400){
        setItemsPerRow(4);
      }
      else if(width >= 901){
        setItemsPerRow(3)
      }
      else if(width >= 768){
        setItemsPerRow(2);
      }
      else{
        setItemsPerRow(1);
      }
    }

    useEffect(() => {
        onResize();
        window.addEventListener("resize", onResize);
    }, []);

    return itemsPerRow;
}
