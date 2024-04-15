import { Skeleton } from "@nextui-org/skeleton";

export default function getGroupSkeletonItems(rowSize){
    let toReturn = [];
    for(let i = 0; 3 > i; i++){
        for(let j = 0; rowSize > j; j++){
            toReturn.push(
                <div className="flex flex-col items-center gap-6 justify-center rounded w-[320px] h-[275px] md:w-[275px] md:h-[250px] mt-[15px] shadow-light">
                    <Skeleton className="rounded w-[70%] h-[150px] before:!duration-1000 ">
                    </Skeleton>
                    <Skeleton className="text-lg rounded-md w-[60%] h-[30px] before:!duration-1000">
                    </Skeleton>
                  </div>
            )
        }
    }
    return toReturn;
}