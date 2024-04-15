import { Skeleton } from "@nextui-org/skeleton";
import "../components/textview/TextView.css"

export default function getTextSkeletonItems(rowSize){
    let toReturn = [];
    for(let i = 0; 3 > i; i++){
        for(let j = 0; rowSize > j; j++){
            toReturn.push(
                <div className="TextView shadow-none">
                    <div className="TextView-Title flex flex-col items-center">
                        <Skeleton className="w-[70%] h-7 rounded-md before:!duration-1000">
                        </Skeleton>
                    </div>
                    <div className='TextView-SubmitButtonContainer'>
                        <Skeleton className=" w-[140px] h-[50px] before:!duration-1000 ">
                        </Skeleton>
                    </div>
                  </div>
            )
        }
    }
    return toReturn;
}