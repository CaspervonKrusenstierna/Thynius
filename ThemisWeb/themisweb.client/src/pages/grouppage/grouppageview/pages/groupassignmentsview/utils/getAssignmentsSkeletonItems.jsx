import { Skeleton } from "@nextui-org/skeleton";

export default function getAssignmentsSkeletonItems(rowSize){
    let toReturn = [];
    for(let i = 0; 3 > i; i++){
        for(let j = 0; rowSize > j; j++){
            toReturn.push(
                <div className="relative flex flex-col gap-4 justify-center items-center rounded w-[280px] h-[260px] md:w-[225px] md:h-[200px] mt-[15px] shadow-light">
                    <Skeleton className="rounded h-[150px] w-[70%] md:h-[120px] before:!duration-1000 ">
                    </Skeleton>
                    <Skeleton className="text-lg rounded-md w-[60%] h-[30px] before:!duration-1000">
                    </Skeleton>
                  </div>
            )
        }
    }
    return toReturn;
}