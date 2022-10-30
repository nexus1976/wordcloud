import * as axios from 'axios';

export interface IWordCountModel {
	word: string,
	count: number
};

export interface ICommentModel {
	id: string,
	comment: string | null,
	commentParsed: string | null,
	commentDate: Date | null
};

export interface IFilterModel {
	page: number,
	pageSize: number,
	fromDate: Date | null,
	toDate: Date | null
};

export async function GetComments(filter: IFilterModel | null): Promise<ICommentModel[] | null> {
	let url: string = process.env.BASE_URL + '/comments';
	if (filter) {
		url += `?page=${filter.page}&pageSize=${filter.pageSize}`;
		if (filter.fromDate) {
			url += `&fromDate=${filter.fromDate.toISOString()}`;
		}
		if (filter.toDate) {
			url += `&fromDate=${filter.toDate.toISOString()}`;
		}
	}
	try {
		const response = await axios.default.get<Array<ICommentModel>>(url);
		console.log('response', response);
		return response?.data;
	} catch (error) {
		console.log(error);
		return null;
	}
};
