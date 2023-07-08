import { Comment } from "./comment.model";

export class Game {
    id: number;
    name: string;
    picture: string;
    description: string;
    minimumSystemRequirements: string;
    price: number;
    GameTypes: GameGenre[] = [];
    comments: Comment[] = [];
    GameCompanies: GameCompany[] = [];
}

export class GameGenre {
    typeId: number;

    constructor(typeId: number) {
        this.typeId = typeId;
    }
}

export class GameCompany {
    companyId: number;

    constructor(companyId: number) {
        this.companyId = companyId;
    }
}

