let arr = [1, 3, 5, 1, 3, 5, 3, 2, 5, 3, 5, 3, 5, 1, 2, 5, 2]

let maxCount = 0;
let mostOccurringElement;

for (let i = 0; i < arr.length; i++) {
    let currentElementCount = 0;
    for (let j = 0; j < arr.length; j++) {
        if (arr[i] === arr[j]) {
            currentElementCount++;
        }
    }

    if (currentElementCount > maxCount) {
        maxCount = currentElementCount;
        mostOccurringElement = arr[i]
    }
}

console.log("Element - ", mostOccurringElement);
console.log("Count - ", maxCount);