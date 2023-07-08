import { CompanyDto } from "../../nomenclatures/companies/models/company-dto.model";
import { GenreDto } from "../../nomenclatures/genres/models/genre-dto.model";

export class SearchDto {
  name: string;
  priceBottomBound: number;
  priceTopBound: number;

  genres: number[] = [];
  companies: number[] = [];
}
